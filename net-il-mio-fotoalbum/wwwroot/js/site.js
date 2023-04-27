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

// Message;

const postMessage = message => axios
    .post("/api/Message", message)
    .then(() => location.href = "/photo/portfolio")
    .catch(err => renderErrors(err.response.data.errors));

const initMessageForm = () => {
    const form = document.querySelector("#message-create-form");

    form.addEventListener("submit", e => {
        e.preventDefault();

        const message = getMessageFromForm(form);
        postMessage(message);
    });
};

const getMessageFromForm = form => {
    const email = form.querySelector("#email").value;
    const textMessage = form.querySelector("#textMessage").value;

    return {
        id: 0,
        email,
        textMessage,
    };
};

const renderErrors = errors => {
    const emailErrors = document.querySelector("#email-errors");
    const textMessageErrors = document.querySelector("#textMessage-errors");

    emailErrors.innerText = errors.Email?.join("\n") ?? "";
    textMessageErrors.innerText = errors.TextMessage?.join("\n") ?? "";
};
