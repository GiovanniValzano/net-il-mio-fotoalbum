// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



const loadPhotos = filter => getPhotos(filter)
    .then(photos => {
        renderPhoto(photos);
        initButtons();
    });

const getPhotos = title => axios
    .get('/api/photo', title ? { params: { title } } : {})
    .then(res => res.data);

const renderPhoto = photos => {
    const photosContainer = document.querySelector(".portfolio-container");

    photosContainer.innerHTML = photos.map(photoComponent).join('');
};

const photoComponent = photo => `
    <div class="personal-card">
        <a href="/Photo/Detail/${photo.id}"><img src="${photo.imgSrc}" /></a>
        <div class="text-container">
            <h2>${photo.title}</h2>
        </div>
    </div>`;

const initFilter = () => {
    const filter = document.querySelector("#photos-filter input");
    filter.addEventListener("input", (e) => loadPhotos(e.target.value))
};

const deletePhoto = id => axios
    .delete(`/api/photo/${id}`)
    .then(() => loadPhotos())

const initButtons = () => {
    const deleteButtons = document.querySelectorAll(".delete-button");

    deleteButtons.forEach(button => {
        button.addEventListener("click", (e) => {
            const id = Number(button.id.split("-")[2]);
            deletePhoto(id);
        });
    })
};
