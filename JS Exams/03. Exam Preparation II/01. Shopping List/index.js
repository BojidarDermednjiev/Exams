function solve(inputFromConsole) {
  const products = inputFromConsole.shift().split(`!`);
  let currentLine;
  const commandRun = {
    Urgent: urgent,
    Unnecessary: unnecessary,
    Correct: correct,
    Rearrange: rearrange,
  };
  while ((currentLine = inputFromConsole.shift()) !== `Go Shopping!`) {
    const currCommand = currentLine.slice(0, currentLine.length);
    const [commandName, ...rest] = currCommand.split(` `);
    commandRun[commandName](...rest);
  }
  console.log(products.join(`, `));
  function urgent(item) {
    if (!products.includes(item)) {
      products.unshift(item);
    }
  }
  function unnecessary(item) {
    if (products.includes(item)) {
      let indexOfItem = products.indexOf(item);
      products.splice(indexOfItem, 1);
    }
  }
  function correct(oldItem, newItem) {
    if (products.includes(oldItem)) {
      let indexOfItem = products.indexOf(oldItem);
      products[indexOfItem] = newItem;
    }
  }
  function rearrange(item) {
    if (products.includes(item)) {
      let indexOfItem = products.indexOf(item);
      let product = products[indexOfItem];
      products.splice(indexOfItem, 1);
      products.push(product);
    }
  }
}
