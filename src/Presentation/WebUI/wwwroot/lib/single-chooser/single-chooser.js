HTMLInputElement.prototype.singleChooser = function () {
    const element = this;

    if (element.type != 'file') throw new Error('available only file type');

    const viewer = element.parentElement;

    element.addEventListener('change', function (e) {

        viewer.classList.remove('loaded');
        viewer.style.backgroundImage = 'unset';

        const reader = new FileReader();
        reader.onload = function () {
            viewer.style.backgroundImage = `url(${reader.result})`;
            viewer.classList.add('loaded');
        }
        reader.readAsDataURL(e.target.files[0]);
    });
}