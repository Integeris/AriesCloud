function startLoader() {
    const loaderContainer = document.getElementById('loader');
    loaderContainer.style.display = 'flex';
}

function stopLoader() {
    const loaderContainer = document.getElementById('loader');
    loaderContainer.classList.add('hide');
    
    setTimeout(function() {
        loaderContainer.style.display = 'none';
        loaderContainer.classList.remove('hide');
    }, 500);
}
