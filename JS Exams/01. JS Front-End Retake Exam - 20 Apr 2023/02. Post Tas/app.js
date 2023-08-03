window.addEventListener("load", solve);

function solve() {
  const tasks = {};
  const inputSelectors = {
    taskTitle: document.getElementById(`task-title`),
    taskCategory: document.getElementById(`task-category`),
    taskContent: document.getElementById(`task-content`),
  };
  const btnSelectors = {
    publishBtn: document.getElementById(`publish-btn`),
  };
  const containers = {
    reviewList: document.getElementById(`review-list`),
    publishedList: document.getElementById(`published-list`),
  };

  btnSelectors.publishBtn.addEventListener(`click`, submitInfo);
  function submitInfo(e) {
    if (Object.values(inputSelectors).some((x) => x.value === ``)) {
      return;
    }
    const task = {
      titleValue: inputSelectors.taskTitle.value,
      categoryValue: inputSelectors.taskCategory.value,
      contentValue: inputSelectors.taskContent.value,
    };
    const post = createElement(
      `li`,
      null,
      null,
      ["rpost"],
      containers.reviewList
    );
    const article = createElement(`article`, null, null, 0, post);
    createElement(`h4`, task.titleValue, null, 0, article);
    createElement(`p`, task.categoryValue, null, 0, article);
    createElement(`p`, task.contentValue, null, 0, article);
    const editBtn = createElement(`button`, `EDIT`, null, ["action-btn"], post);
    editBtn.classList.add(`edit`);
    const postBtn = createElement(`button`, `POST`, null, ["action-btn"], post);
    postBtn.classList.add(`post`);
    editBtn.addEventListener(`click`, () => {
      inputSelectors.taskTitle.value = task.titleValue;
      inputSelectors.taskCategory.value = task.categoryValue;
      inputSelectors.taskContent.value = task.contentValue;
      post.remove();
    });
    postBtn.addEventListener(`click`, () => {
      editBtn.remove();
      postBtn.remove();
      containers.publishedList.appendChild(post);
    });
    inputSelectors.taskTitle.value = ``;
    inputSelectors.taskCategory.value = ``;
    inputSelectors.taskContent.value = ``;
  }
  function createElement(
    type,
    textContent,
    id,
    classes,
    parentNode,
    attributes
  ) {
    let element = document.createElement(type);
    if (classes && classes.length > 0) {
      element.classList.add(...classes);
    }
    if (id) {
      element.setAttribute(`id`, id);
    }
    if (textContent) {
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
