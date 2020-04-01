import React from "react";
import "./Editor.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { InputGroup, FormControl, Button } from "react-bootstrap";
import * as Controller from "./Controller";
import { FileFetch } from "./FileFetch";
import data from "./dataModel";
import {
  IconSaveOK,
  IconDownload,
  IconUpload,
  IconDelete,
  IconAddItem,
  IconNewFile,
} from "./svg";
import { observer } from "mobx-react-lite";
const { articleData } = data;

const Field = [
  ["grade", "年级"],
  ["unit", "单元"],
  ["index", "序号"],
  ["subIndex", "子序号"],
  ["title", "标题"],
  ["author", "作者"],
  ["tag", "标签"],
  ["body", "正文"],
];
export const Editor = observer(() => {
  return (
    <div>
      <div id="controlPanel">
        <Button
          variant="outline-primary"
          id="downloadBtn"
          onClick={Controller.download}
        >
          <IconDownload /> 下载
        </Button>
        <Button
          variant="outline-primary"
          id="uploadBtn"
          onClick={Controller.upload}
        >
          <IconUpload /> 上载
        </Button>
        <Button
          variant="outline-primary"
          id="saveBtn"
          onClick={Controller.newArticle}
        >
          <IconNewFile /> 新建
        </Button>
        <Button
          variant="outline-primary"
          id="saveBtn"
          onClick={Controller.saveArticle}
        >
          <IconSaveOK /> 保存
        </Button>

        <Button
          variant="outline-danger"
          id="deleteBtn"
          onClick={Controller.deleteArticle}
        >
          <IconDelete /> 删除
        </Button>
        <FileFetch />
      </div>
      {Field.slice(0, Field.length - 1, 1).map((i) => (
        <InputGroup className="mb-3" id="inputBox" key={i[0]}>
          <InputGroup.Prepend id="pre">
            <InputGroup.Text id={i[1]}>
              <div className="preBox">{i[1]}</div>
            </InputGroup.Text>
          </InputGroup.Prepend>
          <FormControl
            placeholder={i[0]}
            aria-describedby="basic-addon1"
            value={articleData[i[0]] || ""}
            onChange={(e) => (articleData[i[0]] = e.target.value)}
          />
        </InputGroup>
      ))}
      <InputGroup>
        <InputGroup.Prepend>
          <InputGroup.Text id="preBox">
            <div className="preBox">正文</div>
          </InputGroup.Text>
        </InputGroup.Prepend>
        <FormControl
          placeholder="body (拖拽右下角三角滑块可以调节本文本框大小)"
          as="textarea"
          aria-label="With textarea"
          value={articleData.body || ""}
          onChange={(e) => (articleData.body = e.target.value)}
        />
      </InputGroup>
      {Object.keys(articleData.node).map((key) => (
        <InputGroup className="mb-3" id="inputBox" key={key}>
          <InputGroup.Prepend>
            <InputGroup.Text id="basic-addon1">
              <div className="preBox">{key}</div>
            </InputGroup.Text>
          </InputGroup.Prepend>
          <FormControl
            placeholder="note"
            aria-describedby="basic-addon1"
            value={articleData.node[key]}
          ></FormControl>
          <InputGroup.Append>
            <Button
              variant="outline-secondary"
              onClick={(e) => {
                Controller.deleteNode(key);
              }}
            >
              <IconDelete />
            </Button>
          </InputGroup.Append>
        </InputGroup>
      ))}
      <Button block variant="outline-info" onClick={Controller.addNode}>
        <IconAddItem /> 添加注释
      </Button>
      <br />
    </div>
  );
});
export { Field };
