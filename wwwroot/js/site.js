<<<<<<< HEAD
﻿//document.addEventListener("DOMContentLoaded", function () {
//    generateBoard();
//});

//let board = [];
//let boardSize = 6;
//let gameOver = false;
//let goldPosition = null;
//let score = 0;  // New Score System

//function generateBoard() {
//    gameOver = false;
//    boardSize = parseInt(document.querySelector(".game-board-container").getAttribute("data-board-size")) || 6;
//    board = createEmptyBoard(boardSize);
//    placeMinesAndGold(boardSize);
//    updateUI();
//}

//function createEmptyBoard(size) {
//    let board = [];
//    for (let i = 0; i < size; i++) {
//        board[i] = Array(size).fill(0);
//    }
//    return board;
//}

//function placeMinesAndGold(size) {
//    let difficulty = document.querySelector(".game-board-container").getAttribute("data-difficulty") || "Easy";
//    let mineCount = Math.floor(size * size * (difficulty === "Easy" ? 0.10 : difficulty === "Medium" ? 0.25 : 0.40));

//    for (let i = 0; i < mineCount; i++) {
//        let x, y;
//        do {
//            x = Math.floor(Math.random() * size);
//            y = Math.floor(Math.random() * size);
//        } while (board[x][y] !== 0);

//        board[x][y] = "M";

//        for (let dx = -1; dx <= 1; dx++) {
//            for (let dy = -1; dy <= 1; dy++) {
//                let nx = x + dx, ny = y + dy;
//                if (nx >= 0 && ny >= 0 && nx < size && ny < size && board[nx][ny] !== "M") {
//                    board[nx][ny]++;
//                }
//            }
//        }
//    }

//    let gx, gy;
//    do {
//        gx = Math.floor(Math.random() * size);
//        gy = Math.floor(Math.random() * size);
//    } while (board[gx][gy] !== 0);

//    board[gx][gy] = "G";
//    goldPosition = { x: gx, y: gy };
//}

//function updateUI() {
//    let boardContainer = document.querySelector(".game-board-container");
//    boardContainer.innerHTML = "";

//    for (let x = 0; x < boardSize; x++) {
//        for (let y = 0; y < boardSize; y++) {
//            let button = document.createElement("button");
//            button.className = "game-button";
//            button.setAttribute("data-x", x);
//            button.setAttribute("data-y", y);
//            button.addEventListener("click", () => revealTile(x, y, button));
//            boardContainer.appendChild(button);
//        }
//    }
//}

//function revealTile(x, y, button) {
//    if (gameOver || button.classList.contains("revealed")) return;

//    button.classList.add("revealed");

//    if (board[x][y] === "M") {
//        button.innerHTML = `<img src="/img/Skull.png" alt="Mine">`;
//        revealAllMines();
//        gameOver = true;
//        setTimeout(() => showGameOverScreen(), 1500);
//        return;
//    }

//    if (board[x][y] === "G") {
//        button.innerHTML = `<img src="/img/Gold.png" alt="Gold">`;
//        handleGoldTile();
//        return;
//    }

//    if (board[x][y] > 0) {
//        button.innerHTML = `<img src="/img/Number ${board[x][y]}.png" alt="${board[x][y]}">`;
//    } else {
//        button.innerHTML = `<img src="/img/Tile Flat.png" alt="Empty">`;
//        revealAdjacentTiles(x, y);
//    }

//    checkWinCondition();
//}

//// **🌟 Improved Flood-Fill for Minesweeper Effect**
//function revealAdjacentTiles(x, y) {
//    let queue = [[x, y]];
//    let visited = new Set();

//    while (queue.length > 0) {
//        let [cx, cy] = queue.shift();
//        let key = `${cx},${cy}`;
//        if (visited.has(key)) continue;
//        visited.add(key);

//        let button = document.querySelector(`button[data-x="${cx}"][data-y="${cy}"]`);
//        if (!button || button.classList.contains("revealed")) continue;

//        button.classList.add("revealed");

//        let tileValue = board[cx][cy];

//        if (tileValue === "G") {
//            button.innerHTML = `<img src="/img/Gold.png" alt="Gold">`;
//            return;
//        } else if (tileValue === "M") {
//            button.innerHTML = `<img src="/img/Skull.png" alt="Mine">`;
//            gameOver = true;
//            setTimeout(() => showGameOverScreen(), 1000);
//            return;
//        } else if (tileValue > 0) {
//            button.innerHTML = `<img src="/img/Number ${tileValue}.png" alt="${tileValue}">`;
//            continue;
//        } else {
//            button.innerHTML = `<img src="/img/Tile Flat.png" alt="Empty">`;

//            for (let dx = -1; dx <= 1; dx++) {
//                for (let dy = -1; dy <= 1; dy++) {
//                    let nx = cx + dx, ny = cy + dy;
//                    if (nx >= 0 && ny >= 0 && nx < boardSize && ny < boardSize) {
//                        queue.push([nx, ny]);
//                    }
//                }
//            }
//        }
//    }
//}

//// **🎉 Fun Gold Tile Effect**
//function handleGoldTile() {
//    score += 50;  // Bonus points for finding gold!
//    document.querySelector("#score").textContent = `Score: ${score}`;
//    setTimeout(() => alert("✨ You found GOLD! Bonus Points! ✨"), 500);
//}

//function revealAllMines() {
//    document.querySelectorAll(".game-button").forEach((button) => {
//        let x = button.getAttribute("data-x");
//        let y = button.getAttribute("data-y");

//        if (board[x][y] === "M") {
//            button.innerHTML = `<img src="/img/Skull.png" alt="Mine">`;
//        }
//    });
//}

//function checkWinCondition() {
//    let unrevealedTiles = document.querySelectorAll(".game-button:not(.revealed)").length;
//    let mineCount = board.flat().filter(cell => cell === "M").length;

//    if (unrevealedTiles === mineCount) {
//        showWinScreen();
//        gameOver = true;
//    }
//}

//function showGameOverScreen() {
//    window.location.href = "/User/Lose";
//}

//function showWinScreen() {
//    window.location.href = "/User/Win";
//}

//document.querySelector("#restartButton")?.addEventListener("click", () => {
//    location.reload();
//});
=======
﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
>>>>>>> 4fc9e1d (Upload Minesweeper project with database connection)
