import { Field } from "./Editor";
import data from "./dataModel";
import state from "./dataModel";
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
  state.currentArticleId = id;
  RESTarticle(id);
}

const axios = require("axios");
export function RESTsearch(src = "") {
  axios.get(`/api/search/${src}`).then(function (response) {
    if (response.data !== "failure") {
      state.articleList = response.data;
    }
  });
}
export function RESTarticle(src = "") {
  axios.get(`/api/article/${src}`).then(function (response) {
    if (response.status === 200) {
      for (let i of Field) {
        state.articleData[i[0]] = response.data[i[0]];
      }
      state.articleData.node = response.data.node;
    }
  });
}
export function RESTupdate(src = "", id = "", type = "update") {
  let temp = JSON.parse(src);
  temp.node["id"] = id;
  temp.node["type"] = type;
  axios.post("/api/article", temp).then(function (response) {
    if (response.state !== "bad request" && response.state !== "none") {
      state.currentArticleId = response.data;
    } else {
      console.log(response.state);
      state.currentArticleId = null;
      if (type === "delete") {
        newArticle();
      }
    }
  });
  search();
}
