window.downloadFile = (fileName, content) => {
    const blob = new Blob([content], { type: "text/plain" });
    const link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
    URL.revokeObjectURL(link.href);
};

window.openFilePicker = (id) => {
    const element = document.getElementById(id);
    element.click();
};