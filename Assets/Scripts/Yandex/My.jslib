mergeInto(LibraryManager.library, {

	SaveExtern: function(data){
		var dataString = UTF8ToString(data);
		var myObj = JSON.parse(dataString);
		player.setData(myObj);
	},

	LoadExtern: function(){
		player.getData().then(_data => {
			const myJSON = JSON.stringify(_data);
			myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
		});
	},

	SetToLeaderboard : function(value){
		ysdk.getLeaderboards()
		.then(lb => {
			lb.setLeaderboardScore('TotalDmg', value);
		});
	},

	AddCoinsExtern : function(value){
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
					console.log('Video ad open');
					myGameInstance.SendMessage("Progress", "Pause");
				},
				onRewarded: () => {
					console.log('Rewarded!');
					myGameInstance.SendMessage("Progress", "AddCoins", value);
				},
				onClose: () => {
					console.log('Video ad closed.');
					myGameInstance.SendMessage("Progress", "Continue");
				},
				onClose: (e) => {
					console.log('Error while open video ad:', e);
					myGameInstance.SendMessage("Progress", "Continue");
				}
			}
		})
	},

	ShowAdv : function(){
		ysdk.adv.showFullscreenAdv({
			callbacks: {
				onOpen: () => {
					console.log('Video ad open');
					myGameInstance.SendMessage("Progress", "Pause");
				},
				onClose: function(wasShown) {
					console.log("----------- closed -----------");
					myGameInstance.SendMessage("Progress", "Continue");
				},
				onError: function(error) {
					myGameInstance.SendMessage("Progress", "Continue");
				}
			}
		})
	},
});