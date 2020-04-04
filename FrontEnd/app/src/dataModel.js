import { observable } from "mobx";

const state = observable({
  articleList: {},
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
  showEditor: false,
  searchWord: "",
  searchType: 0,
});

export default state;
