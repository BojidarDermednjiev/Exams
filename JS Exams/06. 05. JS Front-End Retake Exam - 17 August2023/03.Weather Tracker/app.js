const API_URL = `http://localhost:3030/jsonstore/tasks/`;
const loadInfoWeather = document.getElementById(`load-history`);
const list = document.getElementById("list");
let locations = {};
const inputSelectors = {
  location: document.getElementById("location"),
  temperature: document.getElementById("temperature"),
  date: document.getElementById("date"),
};
const addWeather = document.getElementById(`add-weather`);
const editWeather = document.getElementById(`edit-weather`);
loadInfoWeather.addEventListener(`click`, loadInfo);
async function loadInfo(e) {
  if (e) {
    e.preventDefault();
  }
  list.innerHTML = "";
  const responce = await (await fetch(API_URL)).json();
  let id = Object.keys(responce);
  Object.values(responce).forEach((x) => {
    locations = Object.values(responce);
    const divContainer = createElement(
      `div`,
      false,
      x._id,
      ["container"],
      list
    );
    createElement(`h2`, x.location, false, [], divContainer);
    createElement(`h3`, x.date, false, [], divContainer);
    createElement(`h3`, x.temperature, `celsius`, [], divContainer);
    const btnContainer = createElement(
      `div`,
      false,
      false,
      ["buttons-container"],
      divContainer
    );
    const changeBtn = createElement(
      `button`,
      `Change`,
      false,
      ["change-btn"],
      btnContainer
    );
    changeBtn.addEventListener(`click`, async (e) => {
      const currentId = e.currentTarget.parentNode.parentNode.id;
      const currentLocation = locations.find((x) => x._id == currentId);
      Object.keys(inputSelectors).forEach((key) => {
        inputSelectors[key].value = currentLocation[key];
      });
      e.currentTarget.parentNode.parentNode.remove();
      locations.splice(locations.indexOf(currentLocation), 1);
      editWeather.setAttribute("_id", currentId);
      addWeather.disabled = true;
      editWeather.disabled = false;
    });
    const deleteBtn = createElement(
      `button`,
      `Delete`,
      false,
      ["delete-btn"],
      btnContainer
    );
    deleteBtn.addEventListener(`click`, async (e) => {
      const currentId = e.currentTarget.parentNode.parentNode.id;
      const httpHeaders = {
        method: "DELETE",
      };

      await fetch(`${API_URL}${currentId}`, httpHeaders);
      loadInfo();
    });
  });
}
addWeather.addEventListener(`click`, addLocations);
editWeather.addEventListener(`click`, editLocations);
async function addLocations(e) {
  if (e) {
    e.preventDefault();
  }

  if (Object.values(inputSelectors).some((selector) => selector.value === ``)) {
    return;
  }
  const httpHeaders = {
    method: "POST",
    body: JSON.stringify({
      location: inputSelectors.location.value,
      temperature: inputSelectors.temperature.value,
      date: inputSelectors.date.value,
    }),
  };
  await fetch(API_URL, httpHeaders);
  loadInfo();

  Object.values(inputSelectors).forEach((x) => (x.value = ""));
}
async function editLocations(e) {
  if (e) {
    e.preventDefault();
  }
  const id = e.currentTarget.getAttribute("_id");
  const httpHeaders = {
    method: "PATCH",
    body: JSON.stringify({
      location: inputSelectors.location.value,
      temperature: inputSelectors.temperature.value,
      date: inputSelectors.date.value,
      _id: id,
    }),
  };

  await fetch(`${API_URL}${id}`, httpHeaders);
  Object.values(inputSelectors).forEach((x) => (x.value = ""));
  loadInfo();

  editWeather.disabled = true;
  addWeather.disabled = false;
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
