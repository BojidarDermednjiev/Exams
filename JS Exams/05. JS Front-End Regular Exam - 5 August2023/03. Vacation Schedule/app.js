const API_URL = `http://localhost:3030/jsonstore/tasks/`;
const loadVacations = document.getElementById(`load-vacations`);
const container = document.querySelector(`list`);
let vacations = {};
const list = document.getElementById("list");
const inputSelectors = {
  name: document.getElementById("name"),
  days: document.getElementById("num-days"),
  date: document.getElementById("from-date"),
};
const addVacation = document.getElementById(`add-vacation`);
const editVacation = document.getElementById(`edit-vacation`);
loadVacations.addEventListener(`click`, loadInfo);
async function loadInfo(e) {
  e.preventDefault();
  list.innerHTML = "";
  const responce = await (await fetch(API_URL)).json();
  Object.values(responce).forEach((x) => {
    vacations = Object.values(responce);
    const divContainer = createElement(
      `div`,
      false,
      x._id,
      ["container"],
      list
    );
    createElement(`h2`, x.name, false, [], divContainer);
    createElement(`h3`, x.date, false, [], divContainer);
    createElement(`h3`, x.days, false, [], divContainer);
    const changeBtn = createElement(
      `button`,
      `Change`,
      false,
      ["change-btn"],
      divContainer
    );
    const doneBtn = createElement(
      `button`,
      `Done`,
      false,
      ["done-btn"],
      divContainer
    );
    changeBtn.addEventListener(`click`, (e) => {
      const id = e.currentTarget.parentNode.id;
      const currentVacation = vacations.find((x) => x._id == id);
      Object.keys(inputSelectors).forEach((key) => {
        inputSelectors[key].value = currentVacation[key];
      });
      e.currentTarget.parentNode.remove();
      vacations.splice(vacations.indexOf(currentVacation), 1);
      editVacation.setAttribute("_id", id);
      addVacation.disabled = true;
      editVacation.disabled = false;
    });
    doneBtn.addEventListener(`click`, async (e) => {
      if (e) {
        e.preventDefault();
      }
      const vacationToRemove = e.currentTarget.parentNode;

      const httpHeaders = {
        method: "DELETE",
      };

      fetch(`${API_URL}${vacationToRemove.id}`, httpHeaders)
        .then(loadInfo())
        .catch((err) => console.error(err));
    });
  });
  addVacation.addEventListener(`click`, async (e) => {
    if (e) {
      e.preventDefault();
    }
    const httpHeaders = {
      method: "PUT",
      body: JSON.stringify({
        name: inputSelectors.name.value,
        days: inputSelectors.days.value,
        date: inputSelectors.date.value,
      }),
    };
    fetch(`${API_URL}`, httpHeaders)
      .then(loadInfo())
      .catch((err) => console.error(err));

    Object.values(inputSelectors).forEach((input) => (input.value = ""));
  });
  editVacation.addEventListener(`click`, (e) => {
    if (e) {
      e.preventDefault();
    }
    const id = e.currentTarget.getAttribute("_id");
    const httpHeaders = {
      method: "PUT",
      body: JSON.stringify({
        name: inputSelectors.name.value,
        days: inputSelectors.days.value,
        date: inputSelectors.date.value,
        _id: id,
      }),
    };

    fetch(`${API_URL}${id}`, httpHeaders)
      .then(loadInfo())
      .catch((err) => console.error(err));

    editVacation.disabled = true;
    addVacation.disabled = false;
  });
}
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
