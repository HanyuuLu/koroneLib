import React from "react";
import "./Editor.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { InputGroup, FormControl, Button } from "react-bootstrap";
import * as Controller from "./Controller.js";
import { FileFetch } from "./FileFetch";

export function Editor(props) {
  return (
    <div>
      <div id="controlPanel">
        <Button variant="outline-primary" id="saveBtn">
          <svg
            class="bi bi-file-earmark-check"
            width="1em"
            height="1em"
            viewBox="0 0 16 16"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path d="M9 1H4a2 2 0 00-2 2v10a2 2 0 002 2h5v-1H4a1 1 0 01-1-1V3a1 1 0 011-1h5v2.5A1.5 1.5 0 0010.5 6H13v2h1V6L9 1z" />
            <path
              fill-rule="evenodd"
              d="M15.854 10.146a.5.5 0 010 .708l-3 3a.5.5 0 01-.708 0l-1.5-1.5a.5.5 0 01.708-.708l1.146 1.147 2.646-2.647a.5.5 0 01.708 0z"
              clip-rule="evenodd"
            />
          </svg>
          保存
        </Button>
        <Button variant="outline-primary" id="downloadBtn">
          <svg
            class="bi bi-cloud-download"
            width="1em"
            height="1em"
            viewBox="0 0 16 16"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path d="M4.887 5.2l-.964-.165A2.5 2.5 0 103.5 10H6v1H3.5a3.5 3.5 0 11.59-6.95 5.002 5.002 0 119.804 1.98A2.501 2.501 0 0113.5 11H10v-1h3.5a1.5 1.5 0 00.237-2.981L12.7 6.854l.216-1.028a4 4 0 10-7.843-1.587l-.185.96z" />
            <path
              fill-rule="evenodd"
              d="M5 12.5a.5.5 0 01.707 0L8 14.793l2.293-2.293a.5.5 0 11.707.707l-2.646 2.646a.5.5 0 01-.708 0L5 13.207a.5.5 0 010-.707z"
              clip-rule="evenodd"
            />
            <path
              fill-rule="evenodd"
              d="M8 6a.5.5 0 01.5.5v8a.5.5 0 01-1 0v-8A.5.5 0 018 6z"
              clip-rule="evenodd"
            />
          </svg>
          下载
        </Button>
        <Button
          onClick={Controller.upload}
          variant="outline-primary"
          id="uploadBtn"
        >
          <svg
            class="bi bi-cloud-upload"
            width="1em"
            height="1em"
            viewBox="0 0 16 16"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path d="M4.887 6.2l-.964-.165A2.5 2.5 0 103.5 11H6v1H3.5a3.5 3.5 0 11.59-6.95 5.002 5.002 0 119.804 1.98A2.501 2.501 0 0113.5 12H10v-1h3.5a1.5 1.5 0 00.237-2.981L12.7 7.854l.216-1.028a4 4 0 10-7.843-1.587l-.185.96z" />
            <path
              fill-rule="evenodd"
              d="M5 8.854a.5.5 0 00.707 0L8 6.56l2.293 2.293A.5.5 0 1011 8.146L8.354 5.5a.5.5 0 00-.708 0L5 8.146a.5.5 0 000 .708z"
              clip-rule="evenodd"
            />
            <path
              fill-rule="evenodd"
              d="M8 6a.5.5 0 01.5.5v8a.5.5 0 01-1 0v-8A.5.5 0 018 6z"
              clip-rule="evenodd"
            />
          </svg>
          上载
        </Button>
        <Button variant="outline-danger" id="deleteBtn">
          <svg
            class="bi bi-trash"
            width="1em"
            height="1em"
            viewBox="0 0 16 16"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path d="M5.5 5.5A.5.5 0 016 6v6a.5.5 0 01-1 0V6a.5.5 0 01.5-.5zm2.5 0a.5.5 0 01.5.5v6a.5.5 0 01-1 0V6a.5.5 0 01.5-.5zm3 .5a.5.5 0 00-1 0v6a.5.5 0 001 0V6z" />
            <path
              fill-rule="evenodd"
              d="M14.5 3a1 1 0 01-1 1H13v9a2 2 0 01-2 2H5a2 2 0 01-2-2V4h-.5a1 1 0 01-1-1V2a1 1 0 011-1H6a1 1 0 011-1h2a1 1 0 011 1h3.5a1 1 0 011 1v1zM4.118 4L4 4.059V13a1 1 0 001 1h6a1 1 0 001-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"
              clip-rule="evenodd"
            />
          </svg>
          删除
        </Button>
        <FileFetch />
      </div>
      {props.fieldList.slice(0, props.fieldList.length - 1, 1).map((i) => (
        <InputGroup className="mb-3" id="inputBox">
          <InputGroup.Prepend id="pre">
            <InputGroup.Text id={i[1]}>
              <div class="preBox">{i[1]}</div>
            </InputGroup.Text>
          </InputGroup.Prepend>
          <FormControl placeholder={i[0]} aria-describedby="basic-addon1" />
        </InputGroup>
      ))}
      <InputGroup>
        <InputGroup.Prepend>
          <InputGroup.Text id="preBox">
            <div class="preBox">正文</div>
          </InputGroup.Text>
        </InputGroup.Prepend>
        <FormControl
          placeholder="body (拖拽右下角三角滑块可以调节本文本框大小)"
          as="textarea"
          aria-label="With textarea"
        />
      </InputGroup>
      {[...props.dataList.note].map(([tag, content]) => (
        <InputGroup className="mb-3" id="inputBox">
          <InputGroup.Prepend>
            <InputGroup.Text id="basic-addon1">
              <div class="preBox">{tag}</div>
            </InputGroup.Text>
          </InputGroup.Prepend>
          <FormControl
            placeholder="note"
            aria-describedby="basic-addon1"
            Value={content}
          ></FormControl>
          <InputGroup.Append>
            <Button variant="outline-secondary">
              <svg
                class="bi bi-trash"
                width="1em"
                height="1em"
                viewBox="0 0 16 16"
                fill="currentColor"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path d="M5.5 5.5A.5.5 0 016 6v6a.5.5 0 01-1 0V6a.5.5 0 01.5-.5zm2.5 0a.5.5 0 01.5.5v6a.5.5 0 01-1 0V6a.5.5 0 01.5-.5zm3 .5a.5.5 0 00-1 0v6a.5.5 0 001 0V6z" />
                <path
                  fill-rule="evenodd"
                  d="M14.5 3a1 1 0 01-1 1H13v9a2 2 0 01-2 2H5a2 2 0 01-2-2V4h-.5a1 1 0 01-1-1V2a1 1 0 011-1H6a1 1 0 011-1h2a1 1 0 011 1h3.5a1 1 0 011 1v1zM4.118 4L4 4.059V13a1 1 0 001 1h6a1 1 0 001-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"
                  clip-rule="evenodd"
                />
              </svg>
            </Button>
          </InputGroup.Append>
        </InputGroup>
      ))}
      <Button block variant="outline-info">
        <svg
          class="bi bi-plus-square"
          width="1em"
          height="1em"
          viewBox="0 0 16 16"
          fill="currentColor"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            fill-rule="evenodd"
            d="M8 3.5a.5.5 0 01.5.5v4a.5.5 0 01-.5.5H4a.5.5 0 010-1h3.5V4a.5.5 0 01.5-.5z"
            clip-rule="evenodd"
          />
          <path
            fill-rule="evenodd"
            d="M7.5 8a.5.5 0 01.5-.5h4a.5.5 0 010 1H8.5V12a.5.5 0 01-1 0V8z"
            clip-rule="evenodd"
          />
          <path
            fill-rule="evenodd"
            d="M14 1H2a1 1 0 00-1 1v12a1 1 0 001 1h12a1 1 0 001-1V2a1 1 0 00-1-1zM2 0a2 2 0 00-2 2v12a2 2 0 002 2h12a2 2 0 002-2V2a2 2 0 00-2-2H2z"
            clip-rule="evenodd"
          />
        </svg>
        添加注释
      </Button>
      <br />
    </div>
  );
}
