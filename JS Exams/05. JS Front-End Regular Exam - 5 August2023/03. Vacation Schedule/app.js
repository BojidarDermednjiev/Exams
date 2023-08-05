const API_URL = "http://localhost:3030/jsonstore/tasks/";
let vacations = {};
const inputSelectors = {
  name: document.getElementById("name"),
  days: document.getElementById("num-days"),
  date: document.getElementById("from-date"),
};
const list = document.getElementById("list");
const actionBtn = {
  loadBtn: document.getElementById("load-vacations"),
  addBtn: document.getElementById("add-vacation"),
  editBtn: document.getElementById("edit-vacation"),
};
actionBtn.editBtn.addEventListener("click", editVacation);
actionBtn.loadBtn.addEventListener("click", loadVacations);
actionBtn.addBtn.addEventListener("click", addVacation);

async function loadVacations() {
  list.innerHTML = "";
  const responce = await (await fetch(API_URL)).json();
  Object.values(responce).forEach((x) => {
    vacations = Object.values(responce);
    const divApplication = createElement(
      "div",
      list,
      null,
      ["container"],
      x._id
    );
    createElement("h2", divApplication, x.name);
    createElement("h3", divApplication, x.date);
    createElement("h3", divApplication, x.days);

    const changeBtn = createElement("button", divApplication, "Change", [
      "change-btn",
    ]);
    changeBtn.addEventListener("click", changeVacation);

    const doneBtn = createElement("button", divApplication, "Done", [
      "done-btn",
    ]);
    doneBtn.addEventListener("click", doneVacation);
    list.appendChild(divApplication);
  });
}
function addVacation(event) {
  if (event) {
    event.preventDefault();
  }

  const httpHeaders = {
    method: "POST",
    body: JSON.stringify({
      name: inputSelectors.name.value,
      days: inputSelectors.days.value,
      date: inputSelectors.date.value,
    }),
  };
  fetch(`${API_URL}`, httpHeaders)
    .then(loadVacations())
    .catch((err) => console.error(err));

  Object.values(inputSelectors).forEach((input) => (input.value = ""));
}
function editVacation(event) {
  if (event) {
    event.preventDefault();
  }
  const id = event.currentTarget.getAttribute("_id");
  console.log(event.currentTarget);
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
    .then(loadVacations())
    .catch((err) => console.error(err));

  actionBtn.editBtn.disabled = true;
  actionBtn.addBtn.disabled = false;
}
function changeVacation(event) {
  const id = event.currentTarget.parentNode.id;
  const currentVacation = vacations.find((x) => x._id == id);
  Object.keys(inputSelectors).forEach((key) => {
    inputSelectors[key].value = currentVacation[key];
  });

  event.currentTarget.parentNode.remove();
  vacations.splice(vacations.indexOf(currentVacation), 1);
  actionBtn.editBtn.setAttribute("_id", id);
  actionBtn.addBtn.disabled = true;
  actionBtn.editBtn.disabled = false;
}
function doneVacation(event) {
  if (event) {
    event.preventDefault();
  }
  const vacationToRemove = event.currentTarget.parentNode;

  const httpHeaders = {
    method: "DELETE",
  };

  fetch(`${API_URL}${vacationToRemove.id}`, httpHeaders)
    .then(loadVacations())
    .catch((err) => console.error(err));
}
function createElement(
  type,
  parentNode,
  content,
  classes,
  id,
  attributes,
  useInnerHtml
) {
  const htmlElement = document.createElement(type);

  if (content && useInnerHtml) {
    htmlElement.innerHTML = content;
  } else if (content && type !== "input") {
    htmlElement.textContent = content;
  }

  if (content && type === "input") {
    htmlElement.value = content;
  }

  if (classes && classes.length > 0) {
    htmlElement.classList.add(...classes);
  }

  if (id) {
    htmlElement.id = id;
  }

  if (attributes) {
    for (const attribute in attributes) {
      htmlElement.setAttribute(attribute, attributes[attribute]);
    }
  }

  if (parentNode) {
    parentNode.appendChild(htmlElement);
  }

  return htmlElement;
}
