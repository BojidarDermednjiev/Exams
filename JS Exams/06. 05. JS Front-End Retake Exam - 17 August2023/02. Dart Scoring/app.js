window.addEventListener("load", solve);

function solve() {
  const inputSelector = {
    playeName: document.getElementById(`player`),
    score: document.getElementById(`score`),
    round: document.getElementById(`round`),
  };
  const addBtn = document.getElementById(`add-btn`);
  const buttons = document.querySelector(".btn.clear");
  const container = {
    sureList: document.getElementById(`sure-list`),
    scoreboardList: document.getElementById(`scoreboard-list`),
  };
  addBtn.addEventListener(`click`, () => {
    if (
      Object.values(inputSelector).some((selector) => selector.value === ``)
    ) {
      return;
    }
    let player = inputSelector.playeName.value;
    let playerScore = inputSelector.score.value;
    let playerRound = inputSelector.round.value;
    addBtn.disabled = true;
    const items = createElement(
      `li`,
      false,
      false,
      ["dart-item"],
      container.sureList
    );
    const article = createElement(`article`, false, false, [], items);
    createElement(`p`, player, false, [], article);
    createElement(`p`, `Score: ${playerScore}`, false, [], article);
    createElement(`p`, `Round: ${playerRound}`, false, [], article);
    const btnEdit = createElement(
      `button`,
      `edit`,
      false,
      ["btn", "edit"],
      items
    );
    const btnOk = createElement(`button`, `ok`, false, ["btn", "ok"], items);
    btnEdit.addEventListener(`click`, () => {
      inputSelector.playeName.value = player;
      inputSelector.score.value = playerScore;
      inputSelector.round.value = playerRound;
      addBtn.disabled = false;
      items.remove();
    });
    btnOk.addEventListener(`click`, () => {
      container.scoreboardList.appendChild(items);
      addBtn.disabled = false;
      btnEdit.remove();
      btnOk.remove();
    });
    buttons.addEventListener(`click`, () => {
      location.reload();
    });
    inputSelector.playeName.value = ``;
    inputSelector.score.value = ``;
    inputSelector.round.value = ``;
  });

  function createElement(
    type,
    textContent,
    id,
    classes,
    parentNode,
    innerHTML,
    attributes
  ) {
    let element = document.createElement(type);
    if (classes && classes.length > 0) {
      element.classList.add(...classes);
    }
    if (id) {
      element.setAttribute(`id`, id);
    }
    if (innerHTML && textContent) {
      element.innerHTML = textContent;
    } else if (textContent) {
      element.textContent = textContent;
    }
    if (parentNode) {
      parentNode.appendChild(element);
    }
    if (attributes) {
      for (const key in attributes) {
        htmlElement.setAttribute(key, attributes[key]);
      }
    }
    return element;
  }
}
