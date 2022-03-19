/**
 * Creates a multiplication table within the div with id multiplication-table
 *
 */
//  -----------------------------
// |  X   1    2    3    4    5  |
// | 1    1    2    3    4    5  |
// | 2    2    4    6    8    10 |
// | 3    3    6    9    12   15 |
// | 4    4    8    12   16   20 |
// | 5    5    10   15   20   25 |
//  -----------------------------
class MultiplicationTable {
  constructor() {
    this.init();
  }

  /**
   * Performs all the bootstrapping methods for the multiplication table script
   */
  private init(): void {
    const numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    // push the "X" to the first cell of table, as a traditional multiplication table does
    // Then add the numbers to the top so we know what is being multiplied
    const table: string[][] = [["x", ...numbers.map((n) => n.toString())]];

    // fill the table with the multiplications
    for (const outer of numbers) {
      const innerMultiplications: number[] = [];

      for (const inner of numbers) {
        innerMultiplications.push(outer * inner);
      }

      // adding outer to the first index of each row
      table.push([
        outer.toString(),
        ...innerMultiplications.map((n) => n.toString()),
      ]);
    }

    const tableDiv = document.getElementById("multiplication-table");
    const tableRows: string[] = [];
    const rowStart = "<tr>";
    const rowEnd = "</tr>";
    const tableDataStart = "<td>";
    const tableDataEnd = "</td>";

    for (const row of table) {
      const cells: string[] = [];
      for (const cell of row) {
        cells.push(tableDataStart + cell + tableDataEnd);
      }

      const tableRow = rowStart + cells.join("") + rowEnd;
      tableRows.push(tableRow);
    }

    tableDiv.innerHTML = `<table class="table">${tableRows.join("")}</table>`;
  }
}

export default MultiplicationTable;
