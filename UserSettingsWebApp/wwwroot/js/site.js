
function LoadWithSpinner(url, destinationDiv, spinnerDiv, method, successUrl)
{
	$(spinnerDiv).show();

	$.ajax({
		url: url,
		type: method,
		//data: data,
		contentType: 'application/json; charset=utf-8',
		success: function (res)
		{
			if(successUrl == null)
			{
				LoadPartial(destinationDiv, res);
			}
			else
			{
				window.location = successUrl;
			}
			toastr.success("Success");
			$(spinnerDiv).hide();
		},
		error: function (res)
		{
			$(spinnerDiv).hide();

			toastr.error("Error");
		}
	});
}

function LoadPartial(Destination, HTMLContent, contentReady) 
{
	$(Destination).fadeOut(300);
	setTimeout(function ()
	{
		$(Destination).empty();
		$(Destination).html(HTMLContent);
		if (contentReady != null)
		{
			contentReady();
		}
		$(Destination).fadeIn(300);
	}, 300);
}
