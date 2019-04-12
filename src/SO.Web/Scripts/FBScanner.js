//var id = "me";

String.prototype.replaceAll = function (search, replacement) {
	var target = this;
	return target.replace(new RegExp(search, 'g'), replacement);
};

$(document).ready(function () {

	$(document).on("click", ".link", function (e) {

		var url = $(e.currentTarget).data('url');
		var opt = $("#options").find(".var");
		var sync = true;
		for (var i = 0; i < opt.length; i++) {
			var name = $(opt[i]).data('name');
			var value = $(opt[i]).val();
			url = url.replace("{" + name + "}", value);
			if ($(e.currentTarget).data('subject')) {
				url = url.replaceAll("{subject}", $(e.currentTarget).data('subject'));
			}
			if ($(e.currentTarget).data('suffix')) {
				url = url.replaceAll("{suffix}", $(e.currentTarget).data('suffix'));
			}
		}
		url = url.replace(/([^:]\/)\/+/g, "$1");
		if (url.indexOf('{friend}') > -1) {
			var friend = $(e.currentTarget).closest(".table").find(".friend-input").val();
			$.ajax({
				type: "POST",
				url: '/get-fb-id.php',
				data: {
					url: friend
				},
				success: function (res) {
					var data = JSON.parse(res);
					window.open(url.replaceAll("{friend}", data.id).replaceAll("{id}", id));
				}
			});

			sync = false;
		}
		if (sync)
			window.open(url.replaceAll("{id}", id));
	});

	$(document).on("change", ".var", function (e) {
		var option = $(e.target).closest(".option");
		var toggle = $(option).find(".var").find(":selected").data("toggle");
		var cImg = $(option).find(".var").find(":selected").data("img");
		console.log($(option).find(".var").find(":selected").data("default"));
		if ($(option).find(".var").find(":selected").data("default") === undefined) {
			option.addClass("inuse");
		}
		else {
			option.removeClass("inuse");
		}
		if (cImg) {
			option.find(".option-icon").attr("src", cImg);
		}
		else {
			option.find(".option-icon").attr("src", $(option).data('img'));
		}
		$(option).find(".toggle").hide();
		$(option).find(".var").css("width", "100%");
		if (toggle) {
			$(option).find(".var").css("width", "25%");
			$(option).find(".toggle." + toggle).show();
		}
	});

	$(document).on("mouseover", "table", function (e) {
		var id = $(e.currentTarget).attr("id");
		$(".no-" + id).addClass("disabled");
	});

	$(document).on("mouseleave", "table", function (e) {
		var id = $(e.currentTarget).attr("id");
		$(".no-" + id).removeClass("disabled");
	});


});

function popupwindow(url, title, w, h) {
	var left = screen.width/2 - w/2;
	var top = screen.height/2 - h/2;
	return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}
