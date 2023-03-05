
$(document).ready(function () {
    'use strict'

    $(".sweet-delete").click(function (e) {
        e.preventDefault();
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn p-2 m-1 btn-success',
                cancelButton: 'btn p-2 m-1 btn-danger',
                title: 'text-dark'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                //swalWithBootstrapButtons.fire(
                //    'Deleted!',
                //    'Your file has been deleted.',
                //    'success'
                //)
                var url = $(this).attr("href");
                fetch(url).then(response => {
                    if (response.ok) {
                        window.location.reload();
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: $(this).attr("data-name") + "not found!",
                        })
                    }
                });
            } 
        })
    })


})
