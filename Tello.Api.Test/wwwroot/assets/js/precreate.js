$(document).ready(function () {
    'use strict'
    console.log("lets find out");
    $(".sweet-create").click(function (e) {
        e.preventDefault();

        let url = $(this).attr("href");

        fetch(url).then(response => response.text())
            .then(data => {
                    
                console.log(data);

                $("#select-category").css("display", "grid");
            })
    })
})
