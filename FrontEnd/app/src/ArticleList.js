import React from "react";
import { Input, Collapse, Checkbox, Select, Row, Col, Button } from "antd";
import { SearchOutlined } from "@ant-design/icons";
import { ListGroup } from "react-bootstrap";
import data from "./dataModel";
import { observer } from "mobx-react-lite";
import { SelectArticle, search } from "./Controller";
import "./ArticleList.css";
import { Field } from "./Editor";
const { Search } = Input;

let SearchList = [];
for (let i of Field) {
  SearchList.push(i[1]);
}
SearchList.push("注释");

export const ArticleList = observer(() => {
  return (
    <>
      <Collapse bordered={false}>
        <Collapse.Panel header="旧版搜索" key="1">
          <Search
            placeholder="仅支持单关键词"
            onSearch={(value) => search(value)}
          />
          <SearchTypeBox />
        </Collapse.Panel>
      </Collapse>
      <SearchBox />
      <ListGroup>
        {Object.keys(data.articleList).map((key) => (
          <TitleBox id={key} />
        ))}
      </ListGroup>
    </>
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

class SearchBox extends React.Component {
  LimitList = { None: "全局" };

  state = {
    searchList: [],
    limitType: "",
  };
  SearchBox() {
    for (let i in Field) {
      this.LimitList[i[0]] = i[1];
    }
  }
  render() {
    return (
      <>
        <Row>
          <Col>
            <Select defaultValue="None">
              <Select.Option value="Author"> 作者</Select.Option>
            </Select>
          </Col>
          <Col flex="auto">
            <Select
              mode="tags"
              style={{ width: "100%" }}
              placeholder="添加搜索标签，开始搜索"
            >
              {["a", "b"]}
            </Select>
          </Col>
          <Col>
            <Button icon={<SearchOutlined />} />
          </Col>
        </Row>
      </>
    );
  }
}
class SearchTypeBox extends React.Component {
  state = {
    checkedList: SearchList,
    indeterminate: true,
    checkAll: true,
  };

  onChange = (checkedList) => {
    this.setState({
      checkedList,
      indeterminate:
        !!checkedList.length && checkedList.length < SearchList.length,
      checkAll: checkedList.length === SearchList.length,
    });
    this.calc(checkedList);
  };

  calc = (checkedList) => {
    let res = 0;
    for (let i = 0; i < SearchList.length; ++i) {
      if (checkedList.indexOf(SearchList[i]) === -1) {
        res += Math.pow(2, i);
      }
    }
    data.searchType = res;
    console.log(res);
  };
  onCheckAllChange = (e) => {
    this.setState({
      checkedList: e.target.checked ? SearchList : [],
      indeterminate: false,
      checkAll: e.target.checked,
    });
    this.calc(e.target.checked ? SearchList : []);
  };

  render() {
    return (
      <div>
        <div className="site-checkbox-all-wrapper">
          <Checkbox
            indeterminate={this.state.indeterminate}
            onChange={this.onCheckAllChange}
            checked={this.state.checkAll}
          >
            全选
          </Checkbox>
        </div>
        <br />
        <Checkbox.Group
          options={SearchList}
          value={this.state.checkedList}
          onChange={this.onChange}
        />
      </div>
    );
  }
}
