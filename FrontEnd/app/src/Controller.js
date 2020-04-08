import { Field } from "./Editor";
import data from "./dataModel";
import state from "./dataModel";
import { message } from "antd";
let { articleData } = data;

export function upload() {
  let btnFile = document.getElementById("btnFile");
  btnFile.click();
}
export function download() {
  console.log(JSON.stringify(articleData));
  let temp = document.createElement("a");
  temp.download = `${articleData.title}-${articleData.author}.json`;
  temp.style.display = "none";
  let blob = new Blob([JSON.stringify(articleData)]);
  temp.href = URL.createObjectURL(blob);
  document.body.appendChild(temp);
  temp.click();
  document.body.removeChild(temp);
}
export function deleteArticle() {
  RESTupdate(
    JSON.stringify(state.articleData),
    state.currentArticleId,
    "delete"
  );
}
export function newArticle() {
  state.currentArticleId = null;
  for (let i in Field) {
    state.articleData[Field[i][0]] = "";
  }
  state.articleData.node = {};
}
export function saveArticle() {
  RESTupdate(
    JSON.stringify(state.articleData),
    state.currentArticleId,
    "update"
  );
}
export function search(src) {
  RESTsearch(src);
}
export function preciseSearch(src) {
  let dict = {};
  src.forEach((element) => {
    let inf = element.split(":");
    if (!dict[inf[0]]) {
      dict[inf[0]] = [];
    }
    dict[inf[0]].push(inf[1]);
  });
  RESTpreciseSearch(dict);
}
export function loadEditorData(src) {
  for (var i in Field) {
    articleData[Field[i][0]] = src[Field[i][0]];
  }
  articleData["node"] = src["node"];
}
export function addNode() {
  let count = 0;
  while (`N${count}` in articleData.node) {
    ++count;
  }
  articleData.node[`N${count}`] = "";
}
export function deleteNode(tag) {
  if (tag in articleData.node) {
    delete articleData.node[tag];
  }
}
export function SelectArticle(id) {
  data.showEditor = true;
  state.currentArticleId = id;
  RESTarticle(id);
}

const axios = require("axios");
export function RESTsearch(src = "") {
  axios
    .get(`/api/search/${src}`, { params: { type: data.searchType } })
    .then(function (response) {
      if (response.data !== "failure") {
        state.articleList = response.data;
      } else {
        message.error("数据异常");
      }
    })
    .catch(function (error) {
      message.error("网络连接失败");
    });
}
export function RESTpreciseSearch(src) {
  axios
    .get(`/api/search/precise`, { params: { type: JSON.stringify(src) } })
    .then(function (response) {
      if (response.data !== "failure") {
        state.articleList = response.data;
      } else {
        message.error("数据异常");
      }
    })
    .catch(function (error) {
      message.error("网络连接失败");
    });
}
export function RESTarticle(src = "") {
  axios
    .get(`/api/article/${src}`)
    .then(function (response) {
      if (response.status === 200) {
        for (let i of Field) {
          state.articleData[i[0]] = response.data[i[0]];
        }
        state.articleData.node = response.data.node;
      }
    })
    .catch(function (error) {
      message.error("网络连接失败");
    });
}
export function RESTupdate(src = "", id = "", type = "update") {
  let temp = JSON.parse(src);
  temp.node["id"] = id;
  temp.node["type"] = type;
  axios
    .post("/api/article", temp)
    .then(function (response) {
      if (response.state !== "bad request" && response.state !== "none") {
        state.currentArticleId = response.data;
        if (type === "update") {
          message.success("已保存");
        } else if (type === "delete") {
          message.success("已删除");
        } else {
          message.success("操作成功");
        }
      } else {
        console.log(response.state);
        state.currentArticleId = null;
        if (type === "delete") {
          newArticle();
        }
      }
      search(data.searchWord);
    })
    .catch(function (error) {
      message.error("网络连接失败");
    });
}
