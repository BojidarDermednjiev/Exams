function solve(inputFromConsole) {
  const numberOfAstronauts = inputFromConsole.shift();
  const astronautsInfo = inputFromConsole.slice(0, numberOfAstronauts);
  const astronauts = astronautsInfo.reduce((acc, curr) => {
    const [name, oxygenLevel, energyReserves] = curr.split(" ");
    if (!acc.hasOwnProperty(name)) {
      acc[name] = [];
    }
    acc[name] = {
      name,
      oxygenLevel: Number(oxygenLevel),
      energyReserves: Number(energyReserves),
    };
    inputFromConsole.shift();
    return acc;
  }, {});
  const commandManipulation = {
    Explore: explore,
    Refuel: refuel,
    Breathe: breathe,
  };
  let currentLine;
  while ((currentLine = inputFromConsole.shift()) !== "End") {
    const [commandName, ...rest] = currentLine.split(` - `);
    commandManipulation[commandName](...rest);
  }
  function explore(name, energyNeeded) {
    const astronaut = astronauts[name];
    if (astronaut.energyReserves >= energyNeeded) {
      astronaut.energyReserves -= energyNeeded;
      console.log(
        `${astronaut.name} has successfully explored a new area and now has ${astronaut.energyReserves} energy!`
      );
    } else {
      console.log(`${astronaut.name} does not have enough energy to explore!`);
    }
  }
  function refuel(name, amount) {
    const astronaut = astronauts[name];
    const recovered = Math.min(200 - astronaut.energyReserves, Number(amount));
    astronaut.energyReserves += Number(amount);
    if (astronaut.energyReserves > 200) {
      astronaut.energyReserves = 200;
    }
    console.log(
      `${astronaut.name} refueled their energy by ${
        astronaut.energyReserves === 200 ? recovered : amount
      }!`
    );
  }
  function breathe(name, amount) {
    const astronaut = astronauts[name];
    const recovered = Math.min(100 - astronaut.oxygenLevel, Number(amount));
    astronaut.oxygenLevel += Number(amount);
    if (astronaut.oxygenLevel > 100) {
      astronaut.oxygenLevel = 100;
    }
    console.log(
      `${astronaut.name} took a breath and recovered ${
        astronaut.oxygenLevel === 100 ? recovered : amount
      } oxygen!`
    );
  }
  let entries = Object.entries(astronauts);
  for (const [name, info] of entries) {
    console.log(
      `Astronaut: ${name}, Oxygen: ${info.oxygenLevel}, Energy: ${info.energyReserves}`
    );
  }
}
