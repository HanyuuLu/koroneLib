import { observable } from "mobx";

const state = observable({
  articleList: { 98327: "983", 38674: "98475", 329874: "98437" },
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
});

export default state;
