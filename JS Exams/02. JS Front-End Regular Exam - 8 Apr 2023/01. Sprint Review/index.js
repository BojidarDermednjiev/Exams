function solve(inputFromConsole) {
  const ticketLines = Number(inputFromConsole.shift());
  const tickets = inputFromConsole.slice(0, ticketLines);
  const commands = inputFromConsole.slice(ticketLines);
  const board = tickets.reduce((acc, curr) => {
    const [assignee, taskId, title, status, estimatedPoints] = curr.split(`:`);
    if (!acc.hasOwnProperty(assignee)) {
      acc[assignee] = [];
    }
    acc[assignee].push({
      taskId,
      title,
      status,
      estimatedPoints: Number(estimatedPoints),
    });
    return acc;
  }, {});
  const commandRunner = {
    "Add New": addNewTask,
    "Change Status": chnageTaskStatus,
    "Remove Task": removeTask,
  };
  commands.forEach((command) => {
    const [commandName, ...rest] = command.split(`:`);
    commandRunner[commandName](...rest);
  });
  function addNewTask(assignee, taskId, title, status, estimatedPoints) {
    if (!board.hasOwnProperty(assignee)) {
      console.log(`Assignee ${assignee} does not exist on the board!`);
      return;
    }
    board[assignee].push({
      taskId,
      title,
      status,
      estimatedPoints: Number(estimatedPoints),
    });
  }
  function chnageTaskStatus(assignee, taskId, status) {
    if (!board.hasOwnProperty(assignee)) {
      console.log(`Assignee ${assignee} does not exist on the board!`);
      return;
    }
    const task = board[assignee].find((x) => x.taskId === taskId);
    if (!task) {
      console.log(`Task with ID ${taskId} does not exist for ${assignee}!`);
      return;
    }
    task.status = status;
  }
  function removeTask(assignee, index) {
    if (!board.hasOwnProperty(assignee)) {
      console.log(`Assignee ${assignee} does not exist on the board!`);
      return;
    }
    if (index < 0 || index >= board[assignee].length) {
      console.log(`Index is out of range!`);
      return;
    }
    board[assignee].splice(index, 1);
  }
  const toDo = calculateTotalForStatus(`ToDo`);
  const inProgress = calculateTotalForStatus(`In Progress`);
  const codeReview = calculateTotalForStatus(`Code Review`);
  const done = calculateTotalForStatus(`Done`);
  console.log(`ToDo: ${toDo}pts`);
  console.log(`In Progress: ${inProgress}pts`);
  console.log(`Code Review: ${codeReview}pts`);
  console.log(`Done Points: ${done}pts`);
  console.log(
    done >= toDo + inProgress + codeReview
      ? `Sprint was successful!`
      : `Sprint was unsuccessful...`
  );
  function calculateTotalForStatus(status) {
    return Object.values(board)
      .flat()
      .filter((x) => x.status === status)
      .reduce((acc, curr) => {
        return acc + curr.estimatedPoints;
      }, 0);
    // Object.values(board).reduce((acc, curr) => {
    //     const total = curr
    //       .filter((x) => x.status === status)
    //       .reduce((sum, task) => {
    //         return sum + task.estimatedPoints;
    //       }, 0);
    //     return acc + total;
    //   }, 0);
  }
}
