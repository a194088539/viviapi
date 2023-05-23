$(function(){
	ZeroClipboard.config( { moviePath: "/static/js/plugin/zeroClipBoard/ZeroClipboard.swf" } );
	var copyBtn = $("#copyUrlBtn")[0];
	var client = new ZeroClipboard(copyBtn);
	client.on( "load", function(client) {

		client.on( "complete", function(client, args) {
			$(".media-link i").show();
		} );
	} );

});
