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
				},
				onRewarded: () => {
					console.log('Rewarded!');
					myGameInstance.SendMessage("Progress", "AddCoins", value);
				},
				onClose: () => {
					console.log('Video ad closed.');
				},
				onClose: (e) => {
					console.log('Error while open video ad:', e);
				}
			}
		})
	},

	ShowAdv : function(){
		ysdk.adv.showFullscreenAdv({
			callbacks: {
				onClose: function(wasShown) {
					console.log("----------- closed -----------");
				},
				onError: function(error) {
					
				}
			}
		})
	},
});