import dayjs from "dayjs";

class Clock {
  private clock: HTMLElement;

  constructor() {
    this.init();
  }

  /**
   * Performs all the bootstrapping methods for the clock script
   */
  private init(): void {
    this.clock = document.getElementById("clock");
    this.setTime();

    setInterval(() => {
      this.setTime();
    }, 1000);
  }

  private setTime(): void {
    this.clock.innerHTML = dayjs().format("HH:mm:ss");
  }
}

export default Clock;
