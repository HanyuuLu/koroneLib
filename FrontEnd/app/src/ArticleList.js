import React from "react";
import { ListGroup } from "react-bootstrap";
import data from "./dataModel";
import { observer } from "mobx-react-lite";
import { SelectArticle } from "./Controller";

export const ArticleList = observer(() => {
  return (
    <ListGroup variant="flush">
      {/* <ListGroup.Item active onClick={(e) => { SelectArticle(e, 'A') }}>选中标题</ListGroup.Item> */}
      {Object.keys(data.articleList).map((key) => (
        <ListGroup.Item
          key={key}
          onClick={(e) => {
            SelectArticle(key);
          }}
        >
          {data.articleList[key]}
        </ListGroup.Item>
      ))}
    </ListGroup>
  );
});
