# Hosting OpenSilver Applications on Static Web Hosting

OpenSilver applications can be seamlessly hosted on any modern static web hosting service. This tutorial will specifically illustrate how to use GitHub Pages for this purpose. 

Before starting, it's crucial to ensure that your chosen hosting platform supports larger file sizes, proper MIME types, content-type headers, and ideally, HTTPS. Thankfully, most modern hosting services fulfil these criteria.

> **TIP:** If you're interested in hosting the application via IIS, follow this [tutorial](add-site-to-iis.md).

## Steps:

### 1. Publish your .Browser project through Visual Studio 2022 (or later):

![Publish Content Menu](/images/how-to-topics/vs-publish.png)
  
![Choose Folder Location](/images/how-to-topics/vs-folder-location.png)
  
![Finish Configuration](/images/how-to-topics/vs-publish-finish.png)
  
![Start Publishing](/images/how-to-topics/vs-start-publishing.png)

The standalone deployment assets will be published in the `/bin/Release/{TARGET FRAMEWORK}/publish/wwwroot` folder.

**NOTE:** Attempting to run index.html as a file (file:// protocol) in your browser will not work. Instead, use a local or hosted server.

### 2. Sub-path configuration:

If you're planning to host your OpenSilver application on a sub-path (like `https://github-account-name.github.io/sub-path/`), you need to configure it in the index.html file. Simply open index.html in any text editor and adjust the base tag to "/sub-path/":

![Index Html Sub Path](/images/how-to-topics/index-html-sub-path.png)

### 3. Prepare your GitHub Pages repository:

Copy the content of the wwwroot folder to the destination folder of your GitHub Pages repo. In our example, this would be the "sub-path" folder.

### 4. Add a .nojekyll file:

For GitHub pages, it's necessary to add an empty file named .nojekyll in the root of your pages repo. Learn more about this [here](https://github.blog/2009-12-29-bypassing-jekyll-on-github-pages/).

### 5. Commit and Push Changes:

Commit and push the changes. It might take several minutes for the changes to reflect. But, once done, your OpenSilver application should be live at `https://github-account-name.github.io/sub-path/`.

**NOTE:** Trying to access `https://github-account-name.github.io/sub-path/index.html` directly will not work. To enable this, you need to update `YourProjectName.Browser/Pages/Index.cs` by adjusting the Route attribute - `[Route("/index.html")]`.

### Additional MIME Types Configuration:

Bear in mind that some hostings may require additional MIME types configuration:

| MIME Type | Extension |
|---|---|
| application/octet-stream | .blat |
| application/octet-stream | .dll |
| application/octet-stream | .dat |
| application/json | .json |
| application/wasm | .wasm |
| application/font-woff | .woff |
| application/font-woff | .woff2 |

<br />

Congratulations! You've successfully hosted your OpenSilver application on a static web hosting platform. Continue exploring its capabilities and enhancing your web development skills.
