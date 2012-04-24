
function autoCompleteUsersOn(element) {
        $(element).autocomplete({
           source: function (request, response) {
               $.ajax({
                   url: '/Account/GetUsersWithQuery', type: "POST", dataType: "json",
                   data: { query: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                               return { label: item, value: item };
                        }))
                    }
                   })
            },
            minLength: 1
         });
}

function getSearchBox(controller, action) {
    var searchHtml = '<span id=\"searchbox\">
                        <form method=\"POST\" action=\"\/' + controller +'\/' + action + '
                            <input type=\"text\" \/>
                            <input type=\"submit\" \/>
                        </form>
                      </span>';
    $("#searchbox").replaceWith(searchHtml);
}
