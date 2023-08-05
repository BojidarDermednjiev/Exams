window.addEventListener("load", solve);

function solve() {
  const inputSelector = {
    studentName: document.getElementById(`student`),
    UniversityName: document.getElementById(`university`),
    score: document.getElementById(`score`),
  };
  const sumbitbtnNext = document.getElementById(`next-btn`);
  const container = {
    previewList: document.getElementById(`preview-list`),
    candidatesList: document.getElementById(`candidates-list`),
  };
  sumbitbtnNext.addEventListener(`click`, () => {
    if (
      Object.values(inputSelector).some((selector) => selector.value === ``)
    ) {
      return;
    }
    let student = inputSelector.studentName.value;
    let university = inputSelector.UniversityName.value;
    let points = inputSelector.score.value;
    const application = createElement(
      `li`,
      false,
      false,
      ["application"],
      container.previewList
    );
    const article = createElement(`article`, false, false, [], application);
    createElement(`h4`, inputSelector.studentName.value, false, [], article);
    createElement(
      `p`,
      `University: ${inputSelector.UniversityName.value}`,
      false,
      [],
      article
    );
    createElement(
      `p`,
      `Score: ${inputSelector.score.value}`,
      false,
      [],
      article
    );
    const editBtn = createElement(
      `button`,
      `edit`,
      false,
      ["action-btn"],
      application
    );
    editBtn.classList.add(`edit`);
    const applyBtn = createElement(
      `button`,
      `apply`,
      false,
      ["action-btn"],
      application
    );
    applyBtn.classList.add(`apply`);
    applyBtn.addEventListener(`click`, () => {
      container.candidatesList.appendChild(application);
      applyBtn.remove();
      editBtn.remove();
    });
    editBtn.addEventListener(`click`, () => {
      inputSelector.studentName.value = student;
      inputSelector.UniversityName.value = university;
      inputSelector.score.value = points;
      application.remove();
    });
    inputSelector.studentName.value = ``;
    inputSelector.UniversityName.value = ``;
    inputSelector.score.value = ``;
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
