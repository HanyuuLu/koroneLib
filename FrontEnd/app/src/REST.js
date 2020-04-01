import state from "./dataModel";

const axois = require("axios");
const HTTP_ROOT = "http://localhost:8080";
export function RESTsearch(src = "") {
  axois.get(`${HTTP_ROOT}/api/search/${src}`).then(function (response) {
    if (response.status === 200) {
      state.articleList = JSON.parse(response.data);
      console.log(response.data);
    }
  });
}
