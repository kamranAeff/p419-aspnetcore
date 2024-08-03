window.addEventListener('load', onLoad);

let box = null;

function onLoad() {
    console.log('loaded!');

    document.body.addEventListener('mousemove', mouseMove);
}

function mouseMove(e) {

    if (box == null) {
        box = document.querySelector('.box');
    }

    if (box == null || box == undefined) {
        return;
    }

    const { clientX, clientY } = e;
    
    box.style.top = `${clientY}px`;
    box.style.left = `${clientX}px`;
    box.style.backgroundColor = `rgb(${(clientX + clientY) % 255},${clientX % 255},${clientY % 255})`;
}