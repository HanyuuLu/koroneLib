import React from "react";
import { ListGroup } from "react-bootstrap";
import data from "./dataModel";
const { articleList } = data;
function SelectArticle(e, id) {
  alert(`切换到id为${id}的课文`);
}
export function ArticleList() {
  return (
    <ListGroup variant="flush">
      {/* <ListGroup.Item active onClick={(e) => { SelectArticle(e, 'A') }}>选中标题</ListGroup.Item> */}
      {Object.keys(articleList).map((key) => (
        <ListGroup.Item
          onClick={(e) => {
            SelectArticle(e, key);
          }}
        >
          {articleList[key]}
        </ListGroup.Item>
      ))}
    </ListGroup>
  );
}
