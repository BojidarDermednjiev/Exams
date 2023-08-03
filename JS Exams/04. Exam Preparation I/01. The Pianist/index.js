function solve(inputFromConsole) {
  const numberOfThePieces = inputFromConsole.shift();
  const musicalWork = inputFromConsole.slice(0, numberOfThePieces);
  const commands = inputFromConsole.slice(numberOfThePieces);
  const song = musicalWork.reduce((acc, curr) => {
    const [piece, composer, key] = curr.split(`|`);
    if (!acc.hasOwnProperty(piece)) {
      acc[piece] = [];
    }
    acc[piece] = { composer, key };
    return acc;
  }, {});
  const commandRunner = {
    Add: addPiece,
    Remove: removePiece,
    ChangeKey: changePiece,
  };
  commands.forEach((command) => {
    const [commandName, ...rest] = command.split(`|`);
    if (commandName !== `Stop`) {
      commandRunner[commandName](...rest);
    } else {
      return;
    }
  });
  function addPiece(piece, composer, key) {
    if (song.hasOwnProperty(piece)) {
      console.log(`${piece} is already in the collection!`);
      return;
    }
    song[piece] = { piece, composer, key };
    console.log(`${piece} by ${composer} in ${key} added to the collection!`);
  }
  function removePiece(piece) {
    if (!song.hasOwnProperty(piece)) {
      console.log(
        `Invalid operation! ${piece} does not exist in the collection.`
      );
      return;
    }
    delete song[piece];
    console.log(`Successfully removed ${piece}!`);
  }
  function changePiece(piece, newKey) {
    if (!song.hasOwnProperty(piece)) {
      console.log(
        `Invalid operation! ${piece} does not exist in the collection.`
      );
      return;
    }
    song[piece].key = newKey;
    console.log(`Changed the key of ${piece} to ${newKey}!`);
  }
  let entries = Object.entries(song);

  for (const [piece, info] of entries) {
    console.log(`${piece} -> Composer: ${info.composer}, Key: ${info.key}`);
  }
}
solve([
  "3",
  "Fur Elise|Beethoven|A Minor",
  "Moonlight Sonata|Beethoven|C# Minor",
  "Clair de Lune|Debussy|C# Minor",
  "Add|Sonata No.2|Chopin|B Minor",
  "Add|Hungarian Rhapsody No.2|Liszt|C# Minor",
  "Add|Fur Elise|Beethoven|C# Minor",
  "Remove|Clair de Lune",
  "ChangeKey|Moonlight Sonata|C# Major",
  "Stop",
]);
