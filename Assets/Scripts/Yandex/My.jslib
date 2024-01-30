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
});