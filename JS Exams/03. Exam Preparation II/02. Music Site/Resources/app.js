window.addEventListener("load", solve);

function solve() {
  let counter = 0;
  const sectionInfo = {
    genre: document.getElementById(`genre`),
    name: document.getElementById(`name`),
    author: document.getElementById(`author`),
    date: document.getElementById(`date`),
  };
  const containers = {
    collectionsHits: document.querySelector(`.all-hits-container`),
    savedHits: document.querySelector(`.saved-container`),
    likes: document.querySelector(`.likes`),
  };
  const btn = document.getElementById(`add-btn`);
  btn.addEventListener(`click`, (e) => {
    e.preventDefault();
    if (Object.values(sectionInfo).some((selector) => selector.value === ``)) {
      return;
    }
    const div = createElement(
      `div`,
      null,
      null,
      ["hits-info"],
      containers.collectionsHits
    );
    const img = createElement(`img`, false, false, [], div);
    img.src = "./static/img/img.png";
    createElement(`h2`, sectionInfo.genre.value, false, [], div);
    createElement(`h2`, sectionInfo.name.value, false, [], div);
    createElement(`h2`, sectionInfo.author.value, false, [], div);
    createElement(`h3`, sectionInfo.date.value, false, [], div);
    sectionInfo.genre.value = ``;
    sectionInfo.name.value = ``;
    sectionInfo.author.value = ``;
    sectionInfo.date.value = ``;
    const saveBtn = createElement(
      `button`,
      `Save song`,
      false,
      ["save-btn"],
      div
    );
    const likeBtn = createElement(
      `button`,
      `Like song`,
      false,
      ["like-btn"],
      div
    );
    const deleteBtn = createElement(
      `button`,
      `Delete`,
      false,
      ["delete-btn"],
      div
    );
    likeBtn.addEventListener(`click`, () => {
      likeBtn.disabled = true;
      counter++;
      const totalLikesElement = document.querySelector("#total-likes .likes p");
      totalLikesElement.textContent = `Total Likes: ${counter}`;
    });
    saveBtn.addEventListener(`click`, () => {
      likeBtn.remove();
      saveBtn.remove();
      containers.savedHits.appendChild(div);
    });
    deleteBtn.addEventListener(`click`, (e) => {
      e.target.parentElement.remove();
    });
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
