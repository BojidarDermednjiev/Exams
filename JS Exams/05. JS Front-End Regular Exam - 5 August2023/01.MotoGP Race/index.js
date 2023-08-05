function solve(inputFromConsole) {
  const numberOfTheRiders = inputFromConsole.shift();
  const infoRiders = inputFromConsole.slice(0, numberOfTheRiders);
  let riders = infoRiders.reduce((acc, curr) => {
    const [name, fuel, position] = curr.split(`|`);
    if (!acc.hasOwnProperty(name)) {
      acc[name] = [];
    }
    acc[name] = { name, fuel: Number(fuel), position: Number(position) };
    return acc;
  }, {});

  const commandManipulation = {
    StopForFuel: stopForFuel,
    Overtaking: overtaking,
    EngineFail: engineFail,
  };
  const commands = inputFromConsole.slice(numberOfTheRiders);
  commands.forEach((command) => {
    const [currCommand, ...input] = command.split(` - `);
    if (currCommand === `Finish`) {
      return;
    }
    commandManipulation[currCommand](...input);
  });
  function stopForFuel(rider, minFuel, changePosition) {
    const findRider = riders[rider];
    if (findRider.fuel < minFuel) {
      findRider.position = changePosition;
      console.log(
        `${rider} stopped to refuel but lost his position, now he is ${changePosition}.`
      );
      return;
    } else {
      console.log(`${rider} does not need to stop for fuel!`);
    }
  }
  function overtaking(firstRider, secondRider) {
    const riderOne = riders[firstRider];
    const riderTwo = riders[secondRider];
    if (riderOne.position < riderTwo.position) {
      const temp = riders[firstRider].position;
      riders[firstRider].position = riders[secondRider].position;
      riders[secondRider].position = temp;
      console.log(`${firstRider} overtook ${secondRider}!`);
    }
  }
  function engineFail(rider, lapsLeft) {
    if (lapsLeft > 0) {
      delete riders[rider];
      console.log(
        `${rider} is out of the race because of a technical issue, ${lapsLeft} laps before the finish.`
      );
    }
  }
  Object.values(riders).sort((a, b) => a.position - b.position);
  let entries = Object.entries(riders);
  for (const [name, info] of entries) {
    console.log(`${name}`);
    console.log(` Final position ${info.position}`);
  }
}
solve([
  "3",
  "Valentino Rossi|100|1",
  "Marc Marquez|90|2",
  "Jorge Lorenzo|80|3",
  "StopForFuel - Valentino Rossi - 50 - 1",
  "Overtaking - Marc Marquez - Jorge Lorenzo",
  "EngineFail - Marc Marquez - 10",
  "Finish",
]);
