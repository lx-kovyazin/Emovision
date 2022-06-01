
var video = null;
var stream = null;

function setupCameraSource(videoElementId) {
    const constraints = { audio: false, video: true };
    navigator
        .mediaDevices
        .getUserMedia(constraints)
        .then(mediaStream => {
            video = document.getElementById(videoElementId);
            if ("srcObject" in video) {
                video.srcObject = mediaStream;
            } else {
                video.src = window.URL.createObjectURL(mediaStream);
            }
            //return getSettings();
            //stream = mediaStream;


            //recorder = new MediaRecorder(mediaStream);
            //// fires every one second and passes an BlobEvent
            //recorder.ondataavailable = captureFrame;
            //var x = mediaStream.getVideoTracks()[0].getSettings().frameRate;
            //recorder.start(x);
        })
        .catch(errHandler)
        ;
}

function setupFileSource(videoElementId, filePath) {
    video = document.getElementById(videoElementId);
    video.setAttribute('src', filePath);
    video.load();
/*    return getSettings();*/
    //stream = video.captureStream();


    //video.src = window.URL.createObjectURL(filePath);
}

function start() {
    video.play();
}

function stop() {
    video.pause();
    video.currentTime = 0;
}


const blobToBase64 = blob => {
    const reader = new FileReader();
    reader.readAsDataURL(blob);
    return new Promise(resolve => {
        reader.onloadend = () => {
            resolve(reader.result);
        };
    });
};


function captureFrame() {
    const stream = video.captureStream();
    const track = stream.getVideoTracks()[0];
    new ImageCapture(track)
        .takePhoto()
        //.then(async blob => await dotNetInvokeAsync('CaptureImageAsync', JSON.stringify(blob)))
        .then(blobToBase64)
        .then(async blobBase64 =>
            await dotNetInvokeAsync('CaptureImageAsync', blobBase64)
            //blob.text().then(async text =>
            //    await dotNetInvokeAsync('CaptureImageAsync', JSON.parse(text))
            //).catch(errHandler)
            //blob => blob.arrayBuffer()
            //    .then(async buffer => await dotNetInvokeAsync('CaptureImageAsync', new Uint8Array(buffer)))
            //            .catch(errHandler)
        )
        .catch(errHandler);
}

function getSettings() {
    const stream = video.captureStream();
    const track = stream.getVideoTracks()[0];
    return track.getSettings();
}

function errHandler(err) {
    console.log(err.name + ": " + err.message);
}

async function dotNetInvokeAsync(method, args) {
    await DotNet.invokeMethodAsync('EmovisionBlazor', method, args);
}