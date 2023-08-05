window.addEventListener("load", solve);

function solve() {
  const tasks = {};
  const inputSelectors = {
    title: document.getElementById(`title`),
    description: document.getElementById(`description`),
    label: document.querySelector(`#label`),
    points: document.getElementById(`points`),
    assignee: document.getElementById(`assignee`),
  };
  const hiddent = document.getElementById(`task-id`);
  const formSectionBtnSelectors = {
    createTaskBtn: document.getElementById(`create-task-btn`),
    deleteTaskBtn: document.getElementById(`delete-task-btn`),
  };
  const pointsSelector = document.getElementById(`total-sprint-points`);
  const taskSectionSelector = {
    tasksSection: document.getElementById(`tasks-section`),
  };
  const icons = {
    Feature: `&#8865`,
    "Low Priority Bug": `&#9737`,
    "High Priority Bug": `&#9888`,
  };
  const lableClasses = {
    Feature: `feature`,
    "Low Priority Bug": `low-priority`,
    "High Priority Bug": `high-priority`,
  };
  formSectionBtnSelectors.createTaskBtn.addEventListener(`click`, () => {
    if (
      Object.values(inputSelectors).some((selector) => selector.value === ``)
    ) {
      return;
    }
    const task = {
      id: `task-${Object.values(tasks).length + 1}`,
      title: inputSelectors.title.value,
      description: inputSelectors.description.value,
      label: inputSelectors.label.value,
      points: Number(inputSelectors.points.value),
      assignee: inputSelectors.assignee.value,
    };
    tasks[task.id] = task;
    const article = createElement(
      `article`,
      null,
      task.id,
      ["task-card"],
      taskSectionSelector.tasksSection
    );
    const div = createElement(
      `div`,
      `${task.label} ${icons[task.label]}`,
      null,
      ["task-card-lebel", lableClasses[task.label]],
      article,
      true
    );
    createElement(`h3`, task.title, null, ["task-card-title"], article);
    createElement(
      `p`,
      task.description,
      null,
      ["task-card-description"],
      article
    );
    createElement(
      `div`,
      `Estimated at ${task.points}`,
      null,
      ["task-card-points"],
      article
    );
    createElement(
      `div`,
      `Assigned to ${task.assignee}`,
      null,
      ["task-card-assignee"],
      article
    );
    const action = createElement(
      `div`,
      null,
      null,
      ["task-card-actions"],
      article
    );
    const deleteBtn = createElement(`button`, `Delete`, null, 0, action);
    deleteBtn.addEventListener(`click`, (e) => {
      const taskId =
        e.currentTarget.parentElement.parentElement.getAttribute(`id`);
      inputSelectors.title.value = tasks[taskId].title;
      inputSelectors.description.value = tasks[taskId].description;
      inputSelectors.label.value = tasks[taskId].label;
      inputSelectors.points.value = tasks[taskId].points;
      inputSelectors.assignee.value = tasks[taskId].assignee;

      hiddent.value = taskId;
      formSectionBtnSelectors.createTaskBtn.disabled = true;
      formSectionBtnSelectors.deleteTaskBtn.disabled = false;
    });
    const total = Object.values(tasks).reduce(
      (acc, curr) => acc + curr.points,
      0
    );
    pointsSelector.textContent = `Total Points ${total}pts`;
    Object.values(inputSelectors).forEach((selector) => (selector.value = ``));
  });
  formSectionBtnSelectors.deleteTaskBtn.addEventListener(`click`, () => {
    const taskId = hiddent.value;
    const taskElement = document.querySelector(`#${taskId}`);
    taskElement.remove();
    delete tasks[taskId];
    inputSelectors.title.value = ``;
    inputSelectors.description.value = ``;
    inputSelectors.label.value = ``;
    inputSelectors.points.value = ``;
    inputSelectors.assignee.value = ``;
    const total = Object.values(tasks).reduce(
      (acc, curr) => acc + curr.points,
      0
    );
    pointsSelector.textContent = `Total Points ${total}pts`;
    formSectionBtnSelectors.createTaskBtn.disabled = false;
    formSectionBtnSelectors.deleteTaskBtn.disabled = true;
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
