function solve(input) {
  let horses = input.shift().split(`|`);
  for (const currLine of input) {
    let info = currLine.split(` `);
    if (info.includes(`Finish`)) {
      break;
    }
    if (info.includes(`Retake`)) {
      const indexOfOvertakingHorse = horses.indexOf(info[1]);
      const indexOfOvertakenHorse = horses.indexOf(info[2]);
      if (indexOfOvertakingHorse < indexOfOvertakenHorse) {
        horses[indexOfOvertakingHorse] = info[2];
        horses[indexOfOvertakenHorse] = info[1];
        console.log(`${info[1]} retakes ${info[2]}.`);
      }
    }
    if (info.includes(`Trouble`)) {
      const horse = info[1];
      const position = horses.indexOf(horse);
      if (position > 0) {
        const previousHorseName = horses[position - 1];
        horses[position - 1] = horse;
        horses[position] = previousHorseName;
        console.log(`Trouble for ${horse} - drops one position.`);
      }
    }
    if (info.includes(`Rage`)) {
      const horseName = info[1];

      const currentHorseIndex = horses.indexOf(horseName);
      if (currentHorseIndex === horses.length - 2) {
        horses.splice(currentHorseIndex, 1);
        horses.push(horseName);
      } else if (currentHorseIndex !== horses.length - 1) {
        horses.splice(currentHorseIndex + 2 + 1, 0, horseName);
        horses.splice(currentHorseIndex, 1);
      }
      console.log(`${horseName} rages 2 positions ahead.`);
    }
    if (info.includes(`Miracle `)) {
      const lastHorseBeComeTheFirst = horses.shift();
      horses.push(lastHorseBeComeTheFirst);
      console.log(`What a miracle - ${lastHorseBeComeTheFirst} becomes first.`);
    }
  }
  console.log(horses.join("->"));
  console.log(`The winner is: ${horses.pop()}`);
}
solve([
  "Onyx|Domino|Sugar|Fiona",
  "Trouble Onyx",
  "Retake Onyx Sugar",
  "Rage Domino",
  "Miracle",
  "Finish",
]);
