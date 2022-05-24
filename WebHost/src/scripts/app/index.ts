import "../../styles/index.scss";
import $ from "jquery";

declare let window;
window.$ = window.JQuery = $;

import components from "@components/index";
import register from "./utilities/register";

register(...components);
