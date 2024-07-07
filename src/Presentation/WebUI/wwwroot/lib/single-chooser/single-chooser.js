HTMLInputElement.prototype.singleChooser = function (callback) {
    const element = this;

    if (element.type != 'file') {

        if (callback) {
            callback(new Error('available only file type'));
            return;
        }
        else {
            throw new Error('available only file type')
        }
    } const viewer = element.parentElement;

    element.addEventListener('change', function (e) {

        viewer.classList.remove('loaded');
        viewer.style.backgroundImage = 'unset';

        const reader = new FileReader();
        reader.onload = function () {
            viewer.style.backgroundImage = `url(${reader.result})`;
            viewer.classList.add('loaded');
        }

        let acceptFile = element.accept;

        if (acceptFile != null && !RegExp(`^${acceptFile}`).test(e.target.files[0].type)) {
            if (callback) {
                callback(new Error(`available only file type (${acceptFile})`));
                return;
            }
            else {
                throw new Error(`available only file type (${acceptFile})`)
            }
        }

        reader.readAsDataURL(e.target.files[0]);
    });
}