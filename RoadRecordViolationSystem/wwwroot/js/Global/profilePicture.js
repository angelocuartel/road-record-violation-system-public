changeProfile = () => {
  document.querySelector("#pictureOutput").src = URL.createObjectURL(
    document.querySelector("#file-holder").files[0]
  );
};
