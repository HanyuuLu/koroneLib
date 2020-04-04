import { observable } from "mobx";

const state = observable({
  articleList: {
    1: { title: "课文标题", node: ["搜索结果1", "搜索结果2"] },
    2: { title: "课文标题2", node: ["示例3", "示例4"] },
    3: { title: "无结果课文", node: [] },
    4: { title: "无结果课文2", node: [] },
    5: { title: "无结果课文2", node: [] },
    6: { title: "无结果课文2", node: [] },
    7: { title: "无结果课文2", node: [] },
    8: { title: "无结果课文2", node: [] },
  },
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
