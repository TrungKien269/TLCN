
function popupSuccess(title, text) {
    Swal.fire({
        title: title,
        text: text,
        icon: 'success'
    });
}

function popupFail(title, text) {
    Swal.fire({
        icon: 'error',
        title: title,
        text: text
        //footer: '<a href>Why do I have this issue?</a>'
    });
}

function popupConfirm() {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            );
        }
    });
}
