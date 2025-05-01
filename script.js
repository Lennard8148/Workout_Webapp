const API_BASE_URL = "http://localhost:5211/api";
let currentUserId = null;

function register() {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    fetch(`${API_BASE_URL}/auth/register`, {
        method: "POST",
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({ benutzername: username, passwort: password })
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Registrierung fehlgeschlagen");
        }
        alert("Registrierung erfolgreich!");
    })
    .catch(() => alert("Registrierung fehlgeschlagen!"));
}

function login() {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    fetch(`${API_BASE_URL}/auth/login`, {
        method: "POST",
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({ benutzername: username, passwort: password })
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Login fehlgeschlagen");
        }
        return response.json();
    })
    .then(user => {
        currentUserId = user.id;
        document.getElementById("login-container").style.display = "none";
        document.getElementById("tracker-container").style.display = "block";
        fetchWorkouts();
    })
    .catch(() => alert("Login fehlgeschlagen!"));
}

function logout() {
    currentUserId = null;
    document.getElementById("login-container").style.display = "flex";
    document.getElementById("tracker-container").style.display = "none";
}

function fetchWorkouts() {
    fetch(`${API_BASE_URL}/workouts`)
    .then(response => response.json())
    .then(data => {
        const table = document.getElementById("workouts");
        table.innerHTML = "";
        data.forEach(workout => {
            if (workout.benutzerId === currentUserId) {
                const row = `<tr>
                    <td>${workout.id}</td>
                    <td>${workout.übung}</td>
                    <td>${workout.gewicht}</td>
                    <td>${workout.wiederholungen}</td>
                    <td>${new Date(workout.datum).toLocaleString()}</td>
                </tr>`;
                table.innerHTML += row;
            }
        });
    });
}

function addWorkout() {
    const exercise = document.getElementById("exercise").value;
    const weight = document.getElementById("weight").value;
    const reps = document.getElementById("reps").value;

    fetch(`${API_BASE_URL}/workouts`, {
        method: "POST",
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({ benutzerId: currentUserId, übung: exercise, gewicht: weight, wiederholungen: reps })
    }).then(() => fetchWorkouts());
}

function deleteWorkout() {
    const id = document.getElementById("delete-id").value;

    fetch(`${API_BASE_URL}/workouts/${id}`, {
        method: "DELETE"
    }).then(() => fetchWorkouts());
}
