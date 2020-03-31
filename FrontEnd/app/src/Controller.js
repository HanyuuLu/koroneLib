import App from './App';
import { ReactDOM } from 'react-dom'
import React from 'react';
var articleList = new Map()
var editorData = new Map()
const Field = [
    ["grade", "年级"],
    ["unit", "单元"],
    ["index", "序号"],
    ["subIndex", "子序号"],
    ["title", "标题"],
    ["author", "作者"],
    ["tag", "标签"],
    ["body", "正文"]
];

export function upload() {
    let btnFile = document.getElementById("btnFile")
    btnFile.click()
}
export function loadEditorData(src) {
    for (var i in Field) {
        editorData.set(Field[i][0], src[Field[i][0]]);
    }
    updateView();
}
export function updateView()
{
    ReactDOM.render(
        <React.StrictMode>
            <App />
        </React.StrictMode>,
        document.getElementById('root')
    );
}
export { Field, editorData };