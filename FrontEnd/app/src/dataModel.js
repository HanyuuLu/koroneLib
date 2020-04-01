import { observable } from "mobx";

const state = observable({
  articleList: { 暂无数据: "1" },
  articleData: {
    grade: null,
    unit: null,
    index: null,
    subIndex: null,
    title: null,
    author: null,
    tag: null,
    body: null,
    node: {},
  },
  currentArticleId: null,
  searchWord: "",
});

export default state;
