import { Field } from "./Editor";
import data from "./dataModel";
import * as REST from "./REST";
const { articleData } = data;

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
  alert("后端服务正在开发中,暂时不可用");
}
export function newArticle() {
  alert("后端服务正在开发中,暂时不可用");
}
export function saveArticle() {
  alert("后端服务正在开发中,暂时不可用");
}
export function search() {
  REST.RESTsearch();
  alert("后端服务正在开发中,暂时不可用");
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
