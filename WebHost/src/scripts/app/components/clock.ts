import dayjs from "dayjs";
import $ from "jquery";
import { IComponent } from "../types/IComponent";

class Clock {
  static get config(): IComponent {
    return {
      namespace: "clock",
      id: "clock",
      pluginName: "clock",
    };
  }

  $el: JQuery<HTMLElement>;

  constructor(context: HTMLElement) {
    this.$el = $(context);
    this.init();
  }

  public init(): void {
    this.setTime();

    setInterval(() => {
      this.setTime();
    }, 1000);
  }

  private setTime(): void {
    this.$el.text(dayjs().format("HH:mm:ss"));
  }
}

export default Clock;
