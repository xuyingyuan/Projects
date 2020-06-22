///<reference path="player.ts"/>
///<reference path="game.ts"/>

let newGame: Game;

document.getElementById("startGame")!.addEventListener('click', ()=>{
  const player: Player = new Player();
  player.name=Utility.getInputValue('playername');  
  const problemCount: number = Number(Utility.getInputValue('problemCount'));
  const factor: number = Number(Utility.getInputValue('factor'));

  newGame = new Game(player, problemCount, factor);  
  newGame.displayGame();
});

document.getElementById('calculate')!.addEventListener('click', ()=>{
  newGame.calculateScore();
})