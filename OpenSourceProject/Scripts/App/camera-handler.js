function UploadPicture() {
	$.ajax({
		type: 'POST',
		url: ("@Url.Content("~/Login/UploadPicture")/"),
		dataType: 'json',
		success: function (data) {
		}
});
}

function Uploadsubmit() {
	debugger;
	var src = $('img').attr('src');
	src_array = src.split('/');
	src = src_array[4];
	if (src != "") {
		$.ajax({
			type: 'POST',
			url: ("@Url.Content("~/Photo/Index")/"),
		dataType: 'json',
		data: { Imagename: src },
		success: function () { }
	});
	window.opener.location.href = "http://localhost:55694/Photo/Changephoto";
	self.close();}}