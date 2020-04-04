import { observer } from "mobx-react-lite";
import React from "react";
import { PageHeader, Switch } from "antd";
import data from "./dataModel";

import "./NavBar.css";

export const NavBar = observer(() => {
  return (
    <>
      <PageHeader
        className="site-page-header"
        title="KoroneLib"
        subTitle="https://github.com/HanyuuLu/koroneLib"
        extra={[
          <Switch
            checkedChildren="编辑器已显示"
            unCheckedChildren="编辑器已隐藏"
            onChange={(checked) => {
              data.showEditor = checked;
              console.log(checked);
            }}
          />,
        ]}
      />
    </>
  );
});
