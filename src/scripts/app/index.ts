import "../../styles/index.scss";
import MultiplicationTable from "@components/multiplicationTable";
import Clock from "@components/clock";

async function constructClasses<T>(
  classes: Array<{ new (): T }>
): Promise<T[]> {
  return await Promise.all(
    classes.map(async (c) => {
      return new c();
    })
  );
}

constructClasses<unknown>([Clock, MultiplicationTable]);
