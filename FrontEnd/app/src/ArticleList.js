import React from "react";
import { ListGroup } from "react-bootstrap";
import data from "./dataModel";
import { observer } from "mobx-react-lite";
import { SelectArticle } from "./Controller";
import "./ArticleList.css";

export const ArticleList = observer(() => {
  return (
    <ListGroup>
      {/* <ListGroup.Item active onClick={(e) => { SelectArticle(e, 'A') }}>选中标题</ListGroup.Item> */}
      {Object.keys(data.articleList).map((key) => (
        <TitleBox id={key} />
      ))}
    </ListGroup>
  );
});
function TitleBox(props) {
  return (
    <ListGroup.Item
      className="TitleBox"
      key={props.id}
      onClick={(e) => {
        SelectArticle(props.id);
      }}
    >
      {data.articleList[props.id].title}
      <ListGroup variant="flush">
        {data.articleList[props.id].node.map((key) => (
          <ListGroup.Item>{key}</ListGroup.Item>
        ))}
      </ListGroup>
    </ListGroup.Item>
  );
}
